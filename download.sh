#transactions=https://gz.blockchair.com/bitcoin/transactions/blockchair_bitcoin_transactions_20160501.tsv.gz

#inputs="https://gz.blockchair.com/bitcoin/inputs/blockchair_bitcoin_inputs_20160501.tsv.gz"

#outputs="https://gz.blockchair.com/bitcoin/outputs/blockchair_bitcoin_outputs_20160501.tsv.gz"

#blocks="https://gz.blockchair.com/bitcoin/blocks/blockchair_bitcoin_blocks_20160501.tsv.gz"


for x in transactions blocks inputs outputs ; do
    for y in https://gz.blockchair.com/bitcoin/${x}/blockchair_bitcoin_${x}_201605{01..31}.tsv.gz; do
        wget ${y} -P data
        gzip -d data/$(echo ${y} | cut -d "/" -f 6)
    done
done
